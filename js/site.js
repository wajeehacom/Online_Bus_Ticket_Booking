

//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.







//// Write your JavaScript code.





//    // Write your JavaScript code.


//    $(document).ready(function () {
//        $("#loadBookingsButton").click(function () {
//            var $j = jQuery.noConflict();
//            $.ajax({
//                type: "GET",
//                url: "Booking/CheckAllData", // Ensure this URL is correct
//                success: function (response) {
//                    console.log("Response received:", response); // Log the response to inspect data
//                    let html = "";
//                    response.forEach(function (booking) {
//                        html += `<tr>
//                                <td>${booking.source}</td>
//                                <td>${booking.destination}</td>
//                                <td>${booking.departureDate}</td>
//                                <td>${booking.busType}</td>
//                                <td>${booking.name}</td>
//                                <td>${booking.phoneNumber}</td>
//                                <td>${booking.passenger}</td>
//                            </tr>`;
//                    });
//                    $("#bookingTableBody").html(html); // Update table body with booking data
//                },
//                error: function (xhr, status, error) {
//                    console.error("Error:", error); // Log any errors for debugging
//                    alert("Error: " + error); // Display an alert for user visibility
//                }
//            });
//        });
//    });


$(document).ready(function () {
    $("#loadBookingsButton").click(function () {
        $.ajax({
            type: "GET",
            url: "/Booking/CheckAllBooking", // Ensure this URL is correct
            success: function (response) {
                console.log("Response received:", response); // Log the response to inspect data

                // Check if response is an array
                if (Array.isArray(response)) {
                    let html = "";
                    response.forEach(function (booking) {
                        html += `<tr>
                                    <td>${booking.source}</td>
                                    <td>${booking.destination}</td>
                                    <td>${booking.departureDate}</td>
                                    <td>${booking.busType}</td>
                                    <td>${booking.name}</td>
                                    <td>${booking.phoneNumber}</td>
                                    <td>${booking.passenger}</td>
                                </tr>`;
                    });
                    $("#bookingTableBody").html(html); // Update table body with booking data
                } else {
                    console.error("Response is not an array:", response); // Log error if response is not an array
                    alert("Error: Received data is not in expected format.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error); // Log any errors for debugging
                alert("Error: " + error); // Display an alert for user visibility
            }
        });
    });
});
