import { openDialog } from './dialogs'

describe('Electron dialogs', () => {
  describe('#openDialog', () => {
    const app = { focus: () => {}, getPath: () => '/home' }
    const dialog = { showOpenDialog: () => ['/some/path.md'] }

    afterEach(() => {
      if (dialog.showOpenDialog.restore) {
        dialog.showOpenDialog.restore()
      }
    })

    it('brings the app into the foreground', () => {
      sinon.spy(app, 'focus')

      return openDialog(app, dialog).then(() => {
        expect(app.focus.calledOnce).to.be.true
      })
    })

    it('passes along properties to dialog', () => {
      sinon.spy(dialog, 'showOpenDialog')

      return openDialog(app, dialog).then(() => {
        const { title, defaultPath, properties, filters } = dialog.showOpenDialog.lastCall.args[0]
        expect(title).to.not.be.empty
        expect(defaultPath).to.eq('/home')
        expect(properties).to.eql(['openFile'])
        expect(filters[0].extensions).to.eql(['md', 'markdown', 'txt'])
      })
    })

    context('when file selected', () => {
      it('resolves with the selected file path', () => {
        return openDialog(app, dialog).should.eventually.eq('/some/path.md')
      })
    })

    context('when no file selected', () => {
      it('rejects the promise', () => {
        sinon.stub(dialog, 'showOpenDialog', () => undefined)
        return openDialog(app, dialog).should.be.rejected
      })
    })
  })
})
