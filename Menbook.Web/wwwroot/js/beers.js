function hasClass(element, cls) {
  return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
}

function addClass(element, cls) {
  if (!hasClass(element, cls)) {
    element.className += " " + cls;
  }
}

function removeClass(element, cls) {
  if (hasClass(element, cls)) {
    var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
    element.className = element.className.replace(reg, ' ');
  }
}

var pouringLiquid = document.getElementById('beer-pouring-liquid'),
  pourButton = document.getElementById('pour-button'),
  beerTap = document.getElementById('beer-tap-top'),
  beerTapShadow = document.getElementById('beer-tap-top-shadow'),
  beerAudioBack = document.getElementById('beer-audio-back'),
  beerAudioBack2 = new Audio('http://sheav.se/sounds/beer-sound-back.mp3'),
  beerAudioBegin = document.getElementById('beer-audio-begin'),
  isPouring = false;

var liquidAnimation = function(el, speed) {
  var liquidBackgroundPositionArr = getComputedStyle(el).getPropertyValue('background-position').split(" ");
  var liquidBackgroudPosition = parseFloat(liquidBackgroundPositionArr[1]);
  if (liquidBackgroudPosition > 0) {
    el.style.backgroundPosition = "50% " + ((liquidBackgroudPosition - speed) + "%");
    el.style.msBackgroundPositionY = (liquidBackgroudPosition - speed) + "%";
  } else {
    el.style.backgroundPosition = "50% " + "100%";
    el.style.msBackgroundPositionY = "100%";
  }
};
var render = function() {
  if (isPouring) {
    liquidAnimation(pouringLiquid, 0.5);
    addClass(pourButton, 'clicked');
    addClass(pouringLiquid, 'pouring');
    beerTap.setAttribute("class", "clicked");
    beerTapShadow.setAttribute("class", "clicked");
    if (beerAudioBegin.currentTime < beerAudioBegin.duration - 1) {
      beerAudioBegin.play();
    } else {
      if (beerAudioBack.currentTime < beerAudioBack.duration - 1) {
        beerAudioBack.play();
      } else if (beerAudioBack2.currentTime < beerAudioBack2.duration - 2) {
        beerAudioBack2.play();
      } else {
        beerAudioBack.currentTime = 0;
        setTimeout(function() {
          beerAudioBack2.pause();
          beerAudioBack2.currentTime = 0;
        }, 300)
      }
    }
  } else {
    beerAudioBack.pause();
    beerAudioBack2.pause();
    beerAudioBegin.pause();
    removeClass(pourButton, 'clicked');
    removeClass(pouringLiquid, 'pouring');
    beerTap.setAttribute("class", "");
    beerTapShadow.setAttribute("class", "");
  }
  requestAnimationFrame(render)
}
render();

pourButton.addEventListener("mousedown", function(e) {
  isPouring = true;
});
document.documentElement.addEventListener('mouseup', function(e) {
  isPouring = false;
});
window.onload = function(){
  addClass(document.getElementById('instructions-container'),'loaded');
}