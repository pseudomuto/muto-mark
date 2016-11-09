'use strict'

const Window = require('app/ui/components/window')
const React = require('react')
const TestUtils = require('react-addons-test-utils')

describe('Window component', () => {
  let result = null

  beforeEach(() => {
    let component = React.createElement(Window, { file: 'README.md' })
    let renderer = TestUtils.createRenderer()

    renderer.render(component)
    result = renderer.getRenderOutput()
  })

  it('creates a wrapper div with the appropriate class', () => {
    expect(result.type).to.equal('div')
    expect(result.props.className).to.equal('window')
  })

  it('creates a content element', () => {
    let child = result.props.children

    expect(child.type).to.be.instanceof(Function)
    expect(child.props.children).to.be.undefined
  })
})
