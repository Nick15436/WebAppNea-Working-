// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // When the Search button is clicked
    $("#btnSearchTicker").click(function () {
        var query = $("#tickerSearchInput").val();
        var resultsDiv = $("#searchResults");

        resultsDiv.html('<div class="text-center p-3">Searching...</div>');

        // Call the C# Handler
        $.get("/Index?handler=SearchTicker", { query: query }, function (data) {
            resultsDiv.empty();

            if (data.length === 0) {
                resultsDiv.html('<div class="text-warning p-2">No results found.</div>');
                return;
            }

            // Loop through results and create buttons
            $.each(data, function (i, item) {
                var symbol = item['1. symbol'];
                var name = item['2. name'];

                var html = `
                    <div class="list-group-item bg-dark text-white border-secondary d-flex justify-content-between align-items-center">
                        <div>
                            <strong>${symbol}</strong><br>
                            <small class="text-muted">${name}</small>
                        </div>
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