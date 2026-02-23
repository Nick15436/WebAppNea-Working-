// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// This JavaScript code (jQuery) is the messenger between the frontend and the backend. it sends the user's search term to the server without reloading the page.

$(document).ready(function () {
    // This code waits for the search button to be clicked. it only executes when the button is clicked.
    $("#btnSearchTicker").click(function () {
        var query = $("#tickerSearchInput").val(); // Whatever the user has typed into the text box is read and stored in this variable
        var resultsDiv = $("#searchResults");

        resultsDiv.html('<div class="text-center p-3">Searching...</div>'); //This temporarily changes the results area to say searching, so that I (and the user) know that the code works        

        // Call the C# Handler called "SearchTicker", using the input of the user as the parameter. This is the method in my backend that actually sends a request to the API which includes the users' input, and the API returns all closest matching results
        $.get("/Index?handler=SearchTicker", { query: query }, function (data) {
            resultsDiv.empty(); //The results area is cleared, so that it does not say "searching" and is empty again.

            // If no results are found, it says that no results are found.
            if (data.length === 0) {
                resultsDiv.html('<div class="text-warning p-2">No results found.</div>');
                return;
            }

            // If there are results, then loop through all and create buttons for each of them, so that the user can click on them.
            $.each(data, function (i, item) {
                var symbol = item['1. symbol'];
                var name = item['2. name'];

                var html = `
                    <div class="list-group-item bg-dark text-white border-secondary d-flex justify-content-between align-items-center">
                        <div>
                            <strong>${symbol}</strong><br>
                            <small class="text-muted">${name}</small>
                        </div>
                        <!-- If you click the add button, the "AddTicker" handler is called, which takes care of the ticker being added to the database. -->
                        <form method="post" action="/Index?handler=AddTicker">
                            <input type="hidden" name="symbol" value="${symbol}">
                            <input name="__RequestVerificationToken" type="hidden" value="${$('input[name="__RequestVerificationToken"]').val()}" />
                            <button type="submit" class="btn btn-sm btn-success">Add</button>
                        </form>
                    </div>`;

                resultsDiv.append(html);
            });
        });
    });
});