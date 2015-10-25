import App             from "app";
import BrowserWindow   from "browser-window";

const READY_EVENT              = "ready";
const ALL_WINDOWS_CLOSED_EVENT = "window-all-closed";

App.on(ALL_WINDOWS_CLOSED_EVENT, () => {
  if (process.platform != "darwin") {
    App.quit();
  }
});

App.on(READY_EVENT, () => {
  let main = new BrowserWindow({ width: 800, height: 600 });
  main.loadUrl("file://" + __dirname + "/views/index.html");
});
