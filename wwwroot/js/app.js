
function setCookie(key, value) {
  var expires = new Date();
  expires.setMonth(expires.getMonth() + 6);
  document.cookie = key + "=" + value + ";expires=" + expires.toUTCString() + ";path=/";
}

function getCookie(key) {
  var keyValue = document.cookie.match("(^|;) ?" + key + "=([^;]*)(;|$)");
  return keyValue ? keyValue[2] : null;
}

function deleteCookie(key) {
  document.cookie = key + "=;expires=Thu, 01 Jan 1970 00:00:00;path=/";
}

function setFocus(id) {
  setTimeout(function () {
    const element = document.getElementById(id);
    if (element != null) element.focus();
  }, 100);
}

function toggleText(item) {
  if (item.className.indexOf("is-show") === -1) item.classList.add("is-show");
  else item.classList.remove("is-show");
}

function textAutoSize(id) {
  setTimeout(function () {
    console.log("textAutoSize " + id);
    const text = document.getElementById(id);
    if(text != null)
    {
      text.addEventListener("input", setAutoHeight);
      setTextHeight(text);
    }
  }, 100);
}

function setAutoHeight() {
  setTextHeight(this);
}

function setTextHeight(text) {
  text.style.height = "auto";
  text.style.height = text.scrollHeight + "px";
  text.style.overflow = "hidden";
}

function scrollDiv(id, top) {
  setTimeout(function () {
    const element = document.getElementById(id);
    if (element != null) {
      if (top === -1) top = element.scrollHeight;
      element.scrollTo({
        top: top,
        left: 0,
        behavior: "smooth",
      });
    }
  }, 100);
}

function dragScroll(id) {
  setTimeout(function () {
    const element = document.querySelector(id ? id : ".table-container");
    if (!element)
      return;
    
    element.style.cursor = "grab";
    let pos = { top: 0, left: 0, x: 0, y: 0 };
  
    const mouseDownHandler = function (e) {
      element.style.cursor = "grabbing";
      element.style.userSelect = "none";
  
      pos = {
        left: element.scrollLeft,
        top: element.scrollTop,
        // Get the current mouse position
        x: e.clientX,
        y: e.clientY,
      };
  
      document.addEventListener("mousemove", mouseMoveHandler);
      document.addEventListener("mouseup", mouseUpHandler);
    };
  
    const mouseMoveHandler = function (e) {
      // How far the mouse has been moved
      const dx = e.clientX - pos.x;
      const dy = e.clientY - pos.y;
  
      // Scroll the element
      //ele.scrollTop = pos.top - dy;
      element.scrollLeft = pos.left - dx;
    };
  
    const mouseUpHandler = function () {
      element.style.cursor = "grab";
      element.style.removeProperty("user-select");
  
      document.removeEventListener("mousemove", mouseMoveHandler);
      document.removeEventListener("mouseup", mouseUpHandler);
    };
  
    // Attach the handler
    element.addEventListener("mousedown", mouseDownHandler);
  }, 100); 
}

function newTab(link) {
  window.open(link);
}

function goBack() {
  history.back();
}

function tagline(status, message) {
  hideNoti();
  if (message != null && message.length > 0) {
    const color = status === null ? "info" : status === true ? "success" : "danger";
    const notify = document.createElement("div");
    notify.id = "notify";
    notify.innerHTML = `
      <div class="notification is-${color}">
        <a class="delete" onclick="hideNoti()"></a>
        <p>${message}</p>
      </div>`;
    document.querySelector("body").appendChild(notify);
    setTimeout(function () {
      notify.remove();
    }, 10000);
  }
}

function hideNoti() {
  const notify = document.querySelector("#notify");
  if (notify !== null) notify.remove();
}

window.addEventListener("keydown", function (e) {
  if (e.code === "F8") {
    console.log("F8 = Draft");
    const btn = this.document.getElementById("btn_draft");
    if (btn != null) btn.click();
  } else if (e.code === "F9") {
    console.log("F9 = Update");
    const btn = this.document.getElementById("btn_update");
    if (btn != null) btn.click();
  } else if (e.code === "F10") {
    console.log("F10 = Create");
    const btn = this.document.getElementById("btn_create");
    if (btn != null) btn.click();
  }
});