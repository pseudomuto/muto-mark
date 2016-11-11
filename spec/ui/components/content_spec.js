'use strict'

const Content = require('app/ui/components/content').default
const React = require('react')
const TestUtils = require('react-addons-test-utils')

describe('Content component', () => {
  let html = '<h1>Test Content Component</h1>'
  let result = null

  beforeEach(() => {
    let component = React.createElement(Content, { html: html }, null)
    let renderer = TestUtils.createRenderer()

    renderer.render(component)
    result = renderer.getRenderOutput()
  })

  it('creates a div with the appropriate class', () => {
    expect(result.type).to.equal('div')
    expect(result.props.className).to.equal('window__content')
  })

  it('sets the innerHTML to the supplied html property', () => {
    expect(result.props.dangerouslySetInnerHTML).to.eql({ __html: html })
  })
})
