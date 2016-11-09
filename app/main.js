const {app, BrowserWindow, dialog, Menu, Tray} = require('electron')
const path = require('path')

const TEMPLATE = 'file://' + path.join(__dirname, 'assets', 'html', 'file.html')
const ICON = path.join(__dirname, 'assets', 'img', 'trayTemplate.png')

let mainWindow = null
let tray = null

const newWindow = () => {
  let filePaths = dialog.showOpenDialog({
    title: 'Open Markdown File',
    defaultPath: app.getPath('home'),
    filters: [
      { name: 'Markdown files', extensions: ['md', 'markdown', 'txt'] }
    ],
    properties: ['openFile']
  })

  if (filePaths) {
    let selectedFile = filePaths[0]
    let win = new BrowserWindow({ title: selectedFile })
    win.loadURL(TEMPLATE + '#' + selectedFile)
  }
}

app.on('ready', () => {
  mainWindow = new BrowserWindow({ skipTaskbar: true, show: false })

  mainWindow.on('close', () => {
    mainWindow = null
  })

  let menu = Menu.buildFromTemplate([
    { label: 'Open...', click: (item, window) => { newWindow() } },
    { role: 'quit' }
  ])

  tray = new Tray(ICON)
  tray.setToolTip(app.getName())
  tray.setContextMenu(menu)
})

app.on('activate', () => {
  // build recent menu, etc.
})

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})
