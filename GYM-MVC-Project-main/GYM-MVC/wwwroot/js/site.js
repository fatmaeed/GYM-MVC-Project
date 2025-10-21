$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.navbar').addClass('scrolled');
    } else {
        $('.navbar').removeClass('scrolled');
    }
});

$('a[href*="#"]').on('click', function (e) {
    e.preventDefault();

    $('html, body').animate(
        {
            scrollTop: $($(this).attr('href')).offset().top - 70,
        },
        500,
        'linear'
    );
});

$(document).ready(function () {
    $('.carousel').carousel({
        interval: 5000
    });

    function animateOnScroll() {
        $('.animate').each(function () {
            var position = $(this).offset().top;
            var scroll = $(window).scrollTop();
            var windowHeight = $(window).height();

            if (scroll + windowHeight > position) {
                $(this).addClass('fadeInUp');
            }
        });
    }

    animateOnScroll();
    $(window).scroll(animateOnScroll);
});