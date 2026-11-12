using Microsoft.AspNetCore.Mvc;

@model StaffRecallApp.Models.StaffListViewModel
@{
    ViewData["Title"] = "Staff List";
}

< div class= "content-top" >
    < h2 class= "section-title" > Staff Notices </ h2 >

    < !--Search box(top right in the wireframe)-- >
    < form method = "get" asp - controller = "Staff" asp - action = "Index" class= "search-form" >
        < input type = "text" name = "searchTerm" value = "@Model.SearchTerm" placeholder = "Search" />
        < button type = "submit" aria - label = "Search" > &#128269;</button>
    </ form >
</ div >

< !--Notice list: image placeholder on the left, text on the right -->
<ul class= "notice-list" >
    @foreach(var notice in Model.Notices)
    {
        < li class= "notice-item" >
            < div class= "notice-image" aria - hidden = "true" ></ div >
            < div class= "notice-text" >
                < strong > @notice.Title </ strong >
                < span class= "notice-date" > @notice.DateIssued.ToString("dd MMM yyyy") </ span >
                < p > @notice.Description </ p >
            </ div >
        </ li >
    }
</ ul >

@if(!Model.Notices.Any())
{
    < p class= "no-results" > No notices found.</ p >
}

