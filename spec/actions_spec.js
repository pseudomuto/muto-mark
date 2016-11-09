'use strict'

const actions = require('app/actions')

describe('Actions', () => {
  describe('#convertFileToHTML', () => {
    let action = actions.convertFileToHTML('./spec/fixtures/simple.md')

    it('dispatches CONTENT_CHANGED action after processing markdown', () => {
      let dispatch = sinon.spy()
      let expected = {
        type: actions.CONTENT_CHANGED,
        html: '<h1 id="simple-heading">Simple Heading</h1>\n'
      }

      return action(dispatch).then(() => {
        expect(dispatch.calledWith(expected)).to.be.true
      })
    })
  })
})
