'use strict'

describe('Sample', () => {
  it('should pass this test', () => {
    expect(true).to.be.true
  })

  it('should also pass this one', () => {
    let obj = { name: 'value' }
    obj.should.have.property('name')
    obj.should.be.a('object')
  })
})
