import Content from 'app/ui/components/content'
import { createElement } from 'react'
import { createRenderer } from 'react-addons-test-utils'

describe('Content component', () => {
  const html = '<h1>Test Content Component</h1>'
  let result = null

  beforeEach(() => {
    const component = createElement(Content, { html: html }, null)
    const renderer = createRenderer()

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
