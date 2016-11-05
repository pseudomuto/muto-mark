'use strict'

const reducer = require('app/reducer')
const actions = require('app/actions')

describe('Reducer', () => {
  context('when action is unknown', () => {
    it('returns a default state object when not defined', () => {
      let expected = { html: '', updatedAt: null }
      expect(reducer(null, { type: 'unknown' })).to.eql(expected)
      expect(reducer(undefined, { type: 'unknown' })).to.eql(expected)
    })

    it('returns the supplied state when defined', () => {
      let state = { html: '<p>Ohai!</p>', updatedAt: Date.now() }
      expect(reducer(state, { type: 'unknown' })).to.eql(state)
    })
  })

  context('when action is CONTENT_CHANGED', () => {
    let state = null
    let clock = null

    beforeEach(() => {
      clock = sinon.useFakeTimers()
      state = reducer(null, { type: actions.CONTENT_CHANGED, html: '<p>Yeah</p>' })
    })

    afterEach(() => {
      clock.restore()
    })

    it('sets html based on value in action', () => {
      expect(state.html).to.equal('<p>Yeah</p>')
    })

    it('sets updatedAt to the current time', () => {
      expect(state.updatedAt).to.equal(Date.now())
    })
  })
})
