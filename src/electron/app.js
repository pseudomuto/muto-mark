import {app, BrowserWindow, Menu, Tray} from 'electron'
import { openDialog } from './dialogs'
import path from 'path'

const TEMPLATE = 'file://' + path.join(__dirname, 'assets', 'html', 'file.html')
const ICON = path.join(__dirname, 'assets', 'img', 'trayTemplate.png')

let mainWindow = null
let tray = null

const newWindow = () => {
  openDialog(app).then(path => {
    const win = new BrowserWindow({ title: path })
    win.loadURL(TEMPLATE + '#' + path)
  })
}

export const start = () => {
  app.on('ready', () => {
    mainWindow = new BrowserWindow({ skipTaskbar: true, show: false })

    mainWindow.on('close', () => {
      mainWindow = null
    })

    const menu = Menu.buildFromTemplate([
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
}
