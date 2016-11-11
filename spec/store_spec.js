import createStore from 'app/store'

describe('Store', () => {
  it('exports a function', () => {
    expect(createStore).to.be.instanceof(Function)
  })

  it('creates a new store object', () => {
    const store = createStore()
    const expectedFunctions = ['dispatch', 'subscribe', 'getState']

    expectedFunctions.forEach((fn) => {
      expect(store[fn]).to.be.instanceof(Function)
    })
  })
})
