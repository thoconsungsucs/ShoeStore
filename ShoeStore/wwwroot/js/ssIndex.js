$(document).ready(function () {

    $('.btn-outline-dark').on('click', function () {
        var $input = $(this).find('input');
        if ($input.is(':checked')) {
            $(this).css({
                'background-color': '',
                'color': '',
                'border-color': ''
            });
            $input.prop('checked', false);
        } else {
            $(this).css({
                'background-color': 'black',
                'color': 'white',
                'border-color': '#007bff'
            });
            $input.prop('checked', true);
        }
    });

    var filters = {

    }

    var checkboxes = document.querySelectorAll(".filter-checkbox");
    checkboxes.forEach(function (element) {
        element.addEventListener('change', function () {
            var filterType = this.getAttribute('data-filter');
            var value = this.getAttribute('value');
            if (this.checked) {
                if (!filters[filterType]) {
                    filters[filterType] = new Array();
                }
                filters[filterType].push(value);
            } else {
                filters[filterType] = filters[filterType].filter(item => item != value);
            }



            $.ajax({
                type: "GET",
                url: "/Admin/SpecificShoe/Filter",
                data: filters,
                traditional: true,
                success: function (response) {
                    $("#specificShoeList").html(response);
                },
                error: function () {
                    alert("An error occurred while loading the results.");
                }
            });
        });
    });
});



function changeMainImage(element, newSrc, colorShoeId) {
    const cardDiv = element.closest('.card.mb-4');
    const mainImage = cardDiv.querySelector('.card-img-top');
    const link = mainImage.closest('a');
    const startColorShoeIdPos = link.href.indexOf('=') + 1;
    const endColorShoeIdPos = link.href.indexOf('&')
    link.href = link.href.slice(0, startColorShoeIdPos) + colorShoeId + link.href.slice(endColorShoeIdPos);
    mainImage.src = decodeURI(newSrc);
}