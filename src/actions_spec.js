import { CONTENT_CHANGED, convertFileToHTML } from './actions'

describe('Actions', () => {
  describe('#convertFileToHTML', () => {
    const action = convertFileToHTML('./fixtures/simple.md')

    it('dispatches CONTENT_CHANGED action after processing markdown', () => {
      const dispatch = sinon.spy()
      const expected = {
        type: CONTENT_CHANGED,
        html: '<h1 id="simple-heading">Simple Heading</h1>\n'
      }

      return action(dispatch).then(() => {
        expect(dispatch.calledWith(expected)).to.be.true
      })
    })
  })
})
