var reconnectStart = false;

window.addEventListener("offline", () => {
  connecting('danger', "Bạn đang ngoại tuyến.");
});
window.addEventListener("online", () => {
  if (!reconnectStart)
    connecting('success', '');
});

let blazor = null;

document.addEventListener("DOMContentLoaded", () => {
  blazor = new InitialBlazor();
  blazor.start();
});

class InitialBlazor {
  constructor() {
    this.retryIntervalMilliseconds = 3000;
    this.miliseconds = 0;
    this.isCanceled = false;
    this.currentReconnectionProcess = null;
  }

  start() {
    Blazor.start({
      configureSignalR: function (builder) {
        let c = builder.build();
        c.serverTimeoutInMilliseconds = 60000;
        c.keepAliveIntervalInMilliseconds = 15000;
        builder.build = () => {
          return c;
        };
      },
      reconnectionHandler: {
        onConnectionDown: () => {
          if (!reconnectStart) {
            this.currentReconnectionProcess = this.startReconnectProcess();
          }
        },
        onConnectionUp: () => {
          if (this.currentReconnectionProcess) {
            this.currentReconnectionProcess.cancel();
          }
          this.currentReconnectionProcess = null;
        },
      },
    });
  }

  startReconnectProcess() {
    console.log("start reconnect");
    reconnectStart = true;
    this.isCanceled = false;
    this.miliseconds = 0;

    if (window.navigator.onLine)
      connecting('info', "Hệ thống đang tự kết nối lại...");
    
    (async () => {
      while (this.isCanceled == false) {

        await new Promise(resolve => setTimeout(resolve, this.retryIntervalMilliseconds));

        try {
          const result = await Blazor.reconnect();

          if (!result) {
            connecting('danger', "Kết nối hết hạn. Làm mới sau 3 giây...");
            setTimeout(location.reload(), 3000);
            return;
          }

          // Successfully reconnected to the server.
          console.log("Client successfully reconnected!");
          connecting('success', "Đã kết nối lại với máy chủ!");
          this.isCanceled = true;
          reconnectStart = false;
          return;
        }
        catch (e) {
          // Didn't reach the server; try again.
          if (!window.navigator.onLine) {
            connecting('danger', "Bạn đang ngoại tuyến!");
          }
          else {
            if (this.miliseconds >= 60000 * 60 * 6) {
              this.isCanceled = true;
              connecting('danger', "Kết nối hết hạn. Làm mới sau 3 giây...");
              setTimeout(location.reload(), 3000);
              return;
            }
            else {
              console.log("Client didn't reached the server!");
              connecting('warning', "Máy chủ không phản hồi, đang thử lại...");
            }
          }
        }
        this.miliseconds += this.retryIntervalMilliseconds;
      }
    })();

    return {
      cancel: () => {
        this.isCanceled = true;
        reconnectStart = false;
      },
    };
  }
}

function connecting(color, message) {
  const old = document.querySelector("#connect");
  if (old !== null)
    old.remove();
  
  if (message != null && message.length > 0) {
    const connect = document.createElement("div");
    connect.id = "connect";
    connect.innerHTML = `
      <div class="field has-addons">
        <div class="control">
          <span class="button is-small is-rounded is-loading is-${color}">
            <span class="icon">
              <i class="material-icons-outlined">refresh</i>
            </span>
          </span>
        </div>
        <div class="control is-expanded">
          <div class="button is-small is-rounded is-fullwidth is-${color}">${message}</div>
        </div>
      </div>`;
    document.querySelector("body").appendChild(connect);
    setTimeout(function () {
      connect.remove();
    }, 500000);
  }
}