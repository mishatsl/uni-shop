
(function($) {
	"use strict"
    $(window).on('load',function () {
        $("#wait").fadeOut(500);
    });
	// Mobile Nav toggle
	$('.menu-toggle > a').on('click', function (e) {
		e.preventDefault();
		$('#responsive-nav').toggleClass('active');
	})

    

	// Fix cart dropdown from closing
	$('.cart-dropdown').on('click', function (e) {
		e.stopPropagation();
    });

    //$('.cart-dropdown').find('a').on('click', function () {
    //    window.location = $(this).attr('href');
    //});

    //$('.cart-dropdown').on('keydown', function (e) {
    //    e.stopPropagation();
    //});

    //$('.cart-dropdown .cart-btns > a').on('keydown', function (e) {
    //    e.stopPropagation();
    //});

    //$('.cart-dropdown .cart-btns > a').on('click', function (e) {
    //    e.stopPropagation();
    //});

/*$('.cart-dropdown .cart-btns > a').on('click', function() {
    window.location.href = $(this).attr('href');
});*/
	/////////////////////////////////////////

	// Products Slick
	$('.products-slick').each(function() {
		var $this = $(this),
				$nav = $this.attr('data-nav');

		$this.slick({
			slidesToShow: 4,
			slidesToScroll: 1,
			autoplay: true,
			infinite: true,
			speed: 300,
			dots: false,
			arrows: true,
			appendArrows: $nav ? $nav : false,
			responsive: [{
	        breakpoint: 991,
	        settings: {
	          slidesToShow: 2,
	          slidesToScroll: 1,
	        }
	      },
	      {
	        breakpoint: 480,
	        settings: {
	          slidesToShow: 1,
	          slidesToScroll: 1,
	        }
	      },
	    ]
		});
	});

	// Products Widget Slick
	$('.products-widget-slick').each(function() {
		var $this = $(this),
				$nav = $this.attr('data-nav');

		$this.slick({
			infinite: true,
			autoplay: true,
			speed: 300,
			dots: false,
			arrows: true,
			appendArrows: $nav ? $nav : false,
		});
	});

	/////////////////////////////////////////

	// Product Main img Slick
	$('#product-main-img').slick({
    infinite: true,
    speed: 300,
    dots: false,
    arrows: true,
    fade: true,
    asNavFor: '#product-imgs',
  });

	// Product imgs Slick
  $('#product-imgs').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: true,
    centerMode: true,
    focusOnSelect: true,
		centerPadding: 0,
		vertical: true,
    asNavFor: '#product-main-img',
		responsive: [{
        breakpoint: 991,
        settings: {
					vertical: false,
					arrows: false,
					dots: true,
        }
      },
    ]
  });

	// Product img zoom
	var zoomMainProduct = document.getElementById('product-main-img');
	if (zoomMainProduct) {
		$('#product-main-img .product-preview').zoom();
	}

	/////////////////////////////////////////

	// Input number
	$('.input-number').each(function() {
		var $this = $(this),
		$input = $this.find('input[type="number"]'),
		up = $this.find('.qty-up'),
		down = $this.find('.qty-down');

		down.on('click', function () {
			var value = parseInt($input.val()) - 1;
			value = value < 1 ? 1 : value;
			$input.val(value);
			$input.change();
			updatePriceSlider($this , value)
		})

		up.on('click', function () {
			var value = parseInt($input.val()) + 1;
			$input.val(value);
			$input.change();
			updatePriceSlider($this , value)
		})
	});

	var priceInputMax = document.getElementById('price-max'),
			priceInputMin = document.getElementById('price-min');

	/*priceInputMax.addEventListener('change', function(){
		updatePriceSlider($(this).parent() , this.value)
	});

	priceInputMin.addEventListener('change', function(){
		updatePriceSlider($(this).parent() , this.value)
	});*/

	function updatePriceSlider(elem , value) {
		if ( elem.hasClass('price-min') ) {
			console.log('min')
			priceSlider.noUiSlider.set([value, null]);
		} else if ( elem.hasClass('price-max')) {
			console.log('max')
			priceSlider.noUiSlider.set([null, value]);
		}
    }

    var param = window.location.pathname;
    var PMin = 1;
    var PMax = 5000;
    var arr_param = param.split('/');
    var href;
    for (var i = 0; i < arr_param.length; i++) {
        if (arr_param[i].indexOf('PriceMin') !== -1) {
            PMin = arr_param[i].replace('PriceMin', '');
        }
    }

    for (var i = 0; i < arr_param.length; i++) {
        if (arr_param[i].indexOf('PriceMax') !== -1) {
            PMax = arr_param[i].replace('PriceMax','');
        }
    }

	// Price Slider
	var priceSlider = document.getElementById('price-slider');
	if (priceSlider) {
		noUiSlider.create(priceSlider, {
            start: [PMin, PMax],
			connect: true,
			step: 1,
			range: {
				'min': 1,
				'max': 5000
			}
		});

		priceSlider.noUiSlider.on('update', function( values, handle ) {
			var value = values[handle];
			handle ? priceInputMax.value = value : priceInputMin.value = value
		});
	}

    function linkPressed() {
        $(this).siblings('a').click();
    }

    $('#price-button').click(function (e) {
        e.preventDefault();
        var param = window.location.pathname;
        var PriceMin = $('#price-min').val().split('.')[0];
        var PriceMax = $('#price-max').val().split('.')[0];
        var arr_param = param.split('/');
        var IsChanged = false;
        var href;
        for (var i = 0; i < arr_param.length; i++)
        {
            if (arr_param[i].indexOf('PriceMin') !== -1) {
                arr_param[i] = "PriceMin" + PriceMin;
                IsChanged = true;
            }
        }

        for (var i = 0; i < arr_param.length; i++) {
            if (arr_param[i].indexOf('PriceMax') !== -1) {
                arr_param[i] = "PriceMax" + PriceMax;
                IsChanged = true;
            }
        }
        //var PriceMin = $('.noUi-base .noUi-origin:nth-child(1) .noUi-handle').attr('aria-valuetext').split('.')[0];
        //var PriceMax = $('.noUi-base .noUi-origin:nth-child(3) .noUi-handle').attr('aria-valuetext').split('.')[0];
        if (IsChanged) {
            href = arr_param.join('/');
        }
        else {

            href = param + "/PriceMin" + PriceMin + "/PriceMax" + PriceMax;
        }
        window.location.href = href;
    });

$('.input-select').change(function(){
   var category = $(this).children("option:selected").val();
    if(category !=='All Categories')
    {
        var data_autocomplete_source = $('#data-autocomplete-source').attr('data-autocomplete-source');
        var arr_data_autocomplete_source = data_autocomplete_source.split('?');
        if(arr_data_autocomplete_source.length > 1)
        {
            if (arr_data_autocomplete_source[arr_data_autocomplete_source.length-1].indexOf('category')!== -1)
            {
                arr_data_autocomplete_source[arr_data_autocomplete_source.length-1] = 'category=' +  category;
            }
           
        }
         else
         {
            arr_data_autocomplete_source[arr_data_autocomplete_source.length] = 'category=' +  category;
         }
        $('#data-autocomplete-source').attr('data-autocomplete-source',arr_data_autocomplete_source.join('?'));
     }
});


    $('#data-autocomplete-source').val(getUrlParameter('searchParam'));
    if (getUrlParameter('category') !== undefined) {
        $('.input-select').val(getUrlParameter('category'));
    }

    setAutocompleteParam();
    hotDealCount();
    

})(jQuery);

function setAutocompleteParam() {
    var category = getUrlParameter('category');
    if (category !== 'All Categories' && category !== undefined) {
        var data_autocomplete_source = $('#data-autocomplete-source').attr('data-autocomplete-source');
        var arr_data_autocomplete_source = data_autocomplete_source.split('?');
        if (arr_data_autocomplete_source.length > 1) {
            if (arr_data_autocomplete_source[arr_data_autocomplete_source.length - 1].indexOf('category') !== -1) {
                arr_data_autocomplete_source[arr_data_autocomplete_source.length - 1] = 'category=' + category;
            }

        }
        else {
            arr_data_autocomplete_source[arr_data_autocomplete_source.length] = 'category=' + category;
        }
        $('#data-autocomplete-source').attr('data-autocomplete-source', arr_data_autocomplete_source.join('?'));
    }
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]).replace(/\+/g, " ");
        }
    }
};

function hotDealCount() {
    var HotDealTime = getCookie('HotDealActionEndTime');
    var IsHotDeal;
    var currentDate;
    var timerDate;
    var Days, Hours, Minutes, Seconds;
    if (HotDealTime == undefined) {
        IsHotDeal = false;
        return;
    }
    IsHotDeal = true;
    var datetime = new Date();
    var HotDealTimeFirstSiteLoading = getCookie('HotDealTimeFirstSiteLoading');
    if (HotDealTimeFirstSiteLoading == undefined) {
        setCookie('HotDealTimeFirstSiteLoading', datetime.toString(), { expires: HotDealTime.toString() });
        HotDealTimeFirstSiteLoading = datetime.toString();
    }
    //var HotDealTimeArr = HotDealTime.split('-');
    //var HotDealTimeFirstSiteLoadingTimeArr = HotDealTimeFirstSiteLoading.toString().split(" ")[4].split(":");
    
    var HotDealTimeArr = new Date(HotDealTime);
    var HotDealTimeFirstSiteLoadingTimeArr = new Date(HotDealTimeFirstSiteLoading);



    setInterval(function () {
        datetime = new Date();
        currentDate = new Date();
        timerDate = HotDealTimeArr.getTime() - currentDate.getTime();
        if (timerDate >= 0) {
           // timerDate = new Date(timerDate);
            Days = Math.floor(timerDate / 1000 / 60 / 60 / 24);
            Hours = Math.floor((timerDate % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            Minutes = Math.floor((timerDate % (1000 * 60 * 60)) / (1000 * 60));
            Seconds = Math.floor((timerDate % (1000 * 60 )) / (1000));
            $(".hot-deal-countdown li div h3").each(function (index) {
                //switch (index)
                //{
                //    case 0:
                //        $(this).text(HotDealTimeArr[index] - (HotDealTimeFirstSiteLoading.toString().split(" ")[2] - datetime.getDay()));
                //        break;
                //    case 1:
                //        $(this).text(HotDealTimeArr[index] - (HotDealTimeFirstSiteLoadingTimeArr[0] - datetime.getHours()));
                //        break;
                //    case 2:
                //        $(this).text(HotDealTimeArr[index] - (HotDealTimeFirstSiteLoadingTimeArr[1]  - datetime.getMinutes()));

                //        break;
                //    case 3: $(this).text(HotDealTimeArr[index] - (HotDealTimeFirstSiteLoadingTimeArr[2]  - datetime.getSeconds()));

                //        break;
                //    default: break;
                //}
                switch (index) {
                    case 0:
                        $(this).text(Days);
                        break;
                    case 1:
                        $(this).text(Hours);
                        break;
                    case 2:
                        $(this).text(Minutes);
                        break;
                    case 3: $(this).text(Seconds);
                        break;
                    default: break;
                }


            });
        }
    }, 1000);
};


function setCookie(name, value, options) {
    options = options || {};

    var expires = options.expires;

    if (typeof expires == "number" && expires) {
        var d = new Date();
        d.setTime(d.getTime() + expires * 1000);
        expires = options.expires = d;
    }
    if (expires && expires.toUTCString) {
        options.expires = expires.toUTCString();
    }

    value = encodeURIComponent(value);

    var updatedCookie = name + "=" + value;

    for (var propName in options) {
        updatedCookie += "; " + propName;
        var propValue = options[propName];
        if (propValue !== true) {
            updatedCookie += "=" + propValue;
        }
    }

    document.cookie = updatedCookie;
}

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
