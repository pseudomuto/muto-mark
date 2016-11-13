import { dialog } from 'electron'

export const openDialog = (app, dlg = dialog) => {
  app.focus()

  return new Promise((resolve, reject) => {
    const filePaths = dlg.showOpenDialog({
      title: 'Open Markdown File',
      defaultPath: app.getPath('home'),
      filters: [ { name: 'Markdown files', extensions: ['md', 'markdown', 'txt'] } ],
      properties: ['openFile']
    })

    if (!filePaths) {
      reject()
      return
    }

    resolve(filePaths[0])
  })
}
