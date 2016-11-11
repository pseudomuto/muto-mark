'use strict'

const createStore = require('app/store').default

describe('Store', () => {
  it('exports a function', () => {
    expect(createStore).to.be.instanceof(Function)
  })

  it('creates a new store object', () => {
    let store = createStore()
    let expectedFunctions = ['dispatch', 'subscribe', 'getState']

    expectedFunctions.forEach((fn) => {
      expect(store[fn]).to.be.instanceof(Function)
    })
  })
})
