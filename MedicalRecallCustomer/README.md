# DeviceSafe — Customer page

A small **ASP.NET Core (.NET 8)** coursework prototype for the **Medical Device Safety and Recall Notice Platform** live brief.

## Scope

This is **only the customer-facing page**. It displays medical device notices and findings already shared by a review team, lets customers search and sort them, and accepts comments. There is no login, employee dashboard, or publishing workflow yet. In a full system, staff would choose what to share and customer/employee permissions would be different.

Covered user stories: 2 (recent notices), 3 (specific device search), 4 (customer comments), 11 (basic smart suggestions), 12 (January 2026 review search). Story 1's login is **intentionally excluded** by request; the customer view is public.

**All four sample notices are fictional. They are not real medical device alerts and must not be used for safety decisions.** The live brief refers to a provided dataset, but no dataset was supplied with this project. Replace the sample list in `Program.cs` with the approved dataset and staff-shared findings when available.

## Run on a PC

1. Install **.NET 8 SDK** and Visual Studio 2022 (ASP.NET and web development workload), or VS Code.
2. Open `MedicalRecallCustomer.csproj` in Visual Studio.
3. In a terminal in this folder, run:

   ```bash
   dotnet restore
   dotnet run --urls http://localhost:5050
   ```
4. Visit `http://localhost:5050` in a browser.
5. Test searching for `pump`, sorting by date, clicking **January 2026**, and posting a comment.

`wwwroot/index.html` contains the page layout, `wwwroot/styles.css` the design, `wwwroot/app.js` the browser interactions, and `Program.cs` the C# API and sample notices.

## Upload to GitHub and work as a group

1. Create an empty repository at https://github.com/new (for example `medical-device-safety`).
2. Open a terminal in this project folder and run:

   ```bash
   git init
   git add .
   git commit -m "Add customer safety notice page"
   git branch -M main
   git remote add origin https://github.com/YOUR_USERNAME/medical-device-safety.git
   git push -u origin main
   ```

   Replace `YOUR_USERNAME` with your own GitHub username.
3. On GitHub, open **Settings → Collaborators** and invite your group members. They can clone the repo, create a branch, and open pull requests to propose changes.
4. The included GitHub Actions workflow builds the project on every push and pull request.

## Share the website online

GitHub stores code but **GitHub Pages cannot run an ASP.NET Core backend**. To get a public website URL, use a service that supports Docker, for example Render:

1. Create a Render account and select **New → Web Service**.
2. Connect the GitHub repository and choose **Docker** as the runtime.
3. Deploy the service. Render supplies a public HTTPS URL after successful deployment.
4. Send that URL to your friends. They do not need Visual Studio just to view the site.

**Comment storage:** Comments are saved in `App_Data/comments.json` on the server. This works for a single local instance but may be lost on redeployment or server replacement with ephemeral hosting. For real group use, move comments to a persistent database and add moderation, authentication/authorization for staff, and proper security controls. Public comments are not moderated in this prototype.

## Assignment alignment

- Live brief: customer access to shared findings, safety notices, employee information, and customer comments.
- Nice-to-have: basic search suggestions (not an AI search engine).
- Earlier stories: most recent, product/device search, January 2026 filter, comments.
- Not included: employee analysis, staff sharing workflow, report generation, staff permissions, and login (out of scope for this first page).
