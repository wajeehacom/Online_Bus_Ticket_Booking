

$(document).ready(function () {
    $("#loadBookingsButton").click(function () {
        $.ajax({
            type: "GET",
            url: "/Booking/Check", // Ensure this URL is correct
            success: function (response) {
                console.log("Response received:", response); // Log the response to inspect data
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
            },
            error: function (xhr, status, error) {
                console.error("Error:", error); // Log any errors for debugging
                alert("Error: " + error); // Display an alert for user visibility
            }
        });
    });
});
