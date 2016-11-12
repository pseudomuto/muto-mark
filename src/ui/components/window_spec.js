import Window from './window'
import { createElement } from 'react'
import { createRenderer } from 'react-addons-test-utils'

describe('Window component', () => {
  let result = null

  beforeEach(() => {
    const component = createElement(Window, { file: 'README.md', html: '' })
    const renderer = createRenderer()

    renderer.render(component)
    result = renderer.getRenderOutput()
  })

  it('creates a wrapper div with the appropriate class', () => {
    expect(result.type).to.equal('div')
    expect(result.props.className).to.equal('window')
  })

  it('creates a content element', () => {
    const child = result.props.children

    expect(child.type).to.be.instanceof(Function)
    expect(child.props.children).to.be.undefined
  })
})
