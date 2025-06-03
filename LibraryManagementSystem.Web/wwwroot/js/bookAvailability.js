$(document).ready(function () {
    $('#bookSelect').on('change', function () {
        const bookId = $(this).val();

        if (!bookId) {
            $('#availabilityStatus')
                .text("Please select a book.")
                .removeClass("text-success text-danger")
                .addClass("text-secondary");

            $('#submitButton').prop('disabled', true);
            return;
        }

        $.get('/Borrowing/CheckAvailability', { bookId: bookId }, function (data) {
            const status = data.available;
            if (status) {
                $('#availabilityStatus').text("Available")
                    .removeClass("text-danger").addClass("text-success");
                $('#submitButton').prop('disabled', false);
            } else {
                $('#availabilityStatus').text("Borrowed")
                    .removeClass("text-success").addClass("text-danger");
                $('#submitButton').prop('disabled', true);
            }
        }).fail(function () {
            $('#availabilityStatus').text("Error checking status")
                .removeClass("text-success").addClass("text-danger");
            $('#submitButton').prop('disabled', true);
        });
    });
});