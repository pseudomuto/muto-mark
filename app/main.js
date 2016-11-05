const {app, BrowserWindow} = require('electron')
const path = require('path')

let mainWindow = null

app.on('ready', () => {
  mainWindow = new BrowserWindow({ titleBarStyle: 'hidden' })
  mainWindow.loadURL('file://' + path.join(__dirname, 'assets', 'html', 'file.html') + '#README.md')

  mainWindow.on('closed', () => {
    mainWindow = null
  })
})

app.on('activate', () => {
  // build recent menu, etc.
})

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})
