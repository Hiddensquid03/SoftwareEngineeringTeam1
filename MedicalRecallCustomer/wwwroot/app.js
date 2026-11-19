const $ = id => document.getElementById(id);
let activeNotice = null;
let searchDelay;

async function getJson(url, options) {
  const response = await fetch(url, options);
  if (!response.ok) throw new Error('Request failed');
  return response.json();
}

function element(tag, text, className) {
  const node = document.createElement(tag);
  if (text !== undefined) node.textContent = text;
  if (className) node.className = className;
  return node;
}

async function loadNotices() {
  const params = new URLSearchParams({ q: $('search').value, month: $('month').value, sort: $('sort').value });
  const cards = $('cards');
  try {
    const notices = await getJson('/api/notices?' + params);
    cards.replaceChildren();
    $('count').textContent = `${notices.length} shared notice${notices.length === 1 ? '' : 's'} found`;
    $('empty').hidden = notices.length > 0;
    notices.forEach(notice => {
      const card = element('article', undefined, 'card');
      const top = element('div', undefined, 'card-top');
      const risk = element('span', notice.risk + ' risk', 'risk ' + notice.risk.toLowerCase());
      const date = element('span', new Date(notice.date + 'T12:00:00').toLocaleDateString('en-GB', { day: 'numeric', month: 'short', year: 'numeric' }), 'date');
      top.append(risk, date);
      const title = element('h3', notice.device);
      const meta = element('p', `${notice.manufacturer} · ${notice.category}`, 'meta');
      const issue = element('p', notice.issue, 'issue');
      const summary = element('p', notice.summary, 'summary');
      const finding = element('div', undefined, 'finding');
      finding.append(element('strong', 'Shared finding: '), document.createTextNode(notice.finding));
      const button = element('button', 'Read / add comments');
      button.type = 'button';
      button.addEventListener('click', () => openComments(notice));
      card.append(top, title, meta, issue, summary, finding, button);
      cards.append(card);
    });
  } catch {
    cards.replaceChildren();
    $('count').textContent = 'Could not load notices. Please refresh the page.';
  }
}

async function loadSuggestions() {
  const term = $('search').value.trim();
  const suggestions = $('suggestions');
  suggestions.replaceChildren();
  if (!term) return;
  try {
    const values = await getJson('/api/suggestions?q=' + encodeURIComponent(term));
    if ($('search').value.trim() !== term) return;
    values.forEach(value => {
      const button = element('button', value);
      button.type = 'button';
      button.setAttribute('role', 'option');
      button.addEventListener('click', () => {
        $('search').value = value;
        suggestions.replaceChildren();
        loadNotices();
      });
      suggestions.append(button);
    });
  } catch { suggestions.replaceChildren(); }
}

$('search').addEventListener('input', () => {
  clearTimeout(searchDelay);
  searchDelay = setTimeout(() => { loadNotices(); loadSuggestions(); }, 250);
});
$('sort').addEventListener('change', loadNotices);
$('month').addEventListener('change', loadNotices);
$('january').addEventListener('click', () => { $('month').value = '2026-01'; loadNotices(); });
$('clear').addEventListener('click', () => {
  $('search').value = '';
  $('month').value = '';
  $('sort').value = 'newest';
  $('suggestions').replaceChildren();
  loadNotices();
});

async function openComments(notice) {
  activeNotice = notice;
  $('dialogTitle').textContent = notice.device;
  $('dialogFinding').textContent = 'Company finding: ' + notice.finding;
  $('commentStatus').textContent = '';
  $('commentForm').reset();
  $('commentsDialog').showModal();
  await loadComments();
}

async function loadComments() {
  const list = $('commentsList');
  list.textContent = 'Loading comments…';
  try {
    const comments = await getJson(`/api/notices/${activeNotice.id}/comments`);
    list.replaceChildren();
    if (!comments.length) { list.textContent = 'No comments yet.'; return; }
    comments.forEach(comment => {
      const item = element('div', undefined, 'comment');
      item.append(element('strong', comment.name), element('small', ' · ' + new Date(comment.createdAt).toLocaleDateString('en-GB')), element('p', comment.message));
      list.append(item);
    });
  } catch { list.textContent = 'Could not load comments.'; }
}

$('commentForm').addEventListener('submit', async event => {
  event.preventDefault();
  if (!activeNotice) return;
  const button = $('commentForm').querySelector('button[type=submit]');
  button.disabled = true;
  try {
    await getJson(`/api/notices/${activeNotice.id}/comments`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name: $('name').value, message: $('message').value })
    });
    $('commentForm').reset();
    await loadComments();
    $('commentStatus').textContent = 'Comment added.';
  } catch { $('commentStatus').textContent = 'Could not save your comment. Please try again.'; }
  finally { button.disabled = false; }
});
$('close').addEventListener('click', () => $('commentsDialog').close());
loadNotices();
