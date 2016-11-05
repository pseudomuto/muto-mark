'use strict'

const React = require('react')
const Content = require('./content')

const create = React.createElement

class Window extends React.Component {
  render () {
    let html = this.props.html
    if (!html) html = '<h1>Loaded</h1>'

    return create(
      'div',
      { className: 'window' },
      create('h1', { className: 'window__heading' }, 'README.md'),
      create(Content, { html: html }, null)
    )
  }
}

Window.propTypes = {
  file: React.PropTypes.string.isRequired,
  html: React.PropTypes.string
}

module.exports = Window
