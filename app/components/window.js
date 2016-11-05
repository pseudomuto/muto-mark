'use strict'

const React = require('react')
const Content = require('./content')

const create = React.createElement

class Window extends React.Component {
  componentDidMount () {
    this.props.actions.convertFileToHTML(this.props.file)
  }

  render () {
    let html = this.props.html

    return create(
      'div',
      { className: 'window' },
      create('h1', { className: 'window__heading' }, this.props.file),
      create(Content, { html })
    )
  }
}

module.exports = Window
