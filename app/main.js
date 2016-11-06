const {app, BrowserWindow} = require('electron')
const path = require('path')

const TEMPLATE = 'file://' + path.join(__dirname, 'assets', 'html', 'file.html')

let mainWindow = null

app.on('ready', () => {
  mainWindow = new BrowserWindow({ title: 'spec/fixtures/github.md' })
  mainWindow.loadURL(TEMPLATE + '#spec/fixtures/github.md')

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
