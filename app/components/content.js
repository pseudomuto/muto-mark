'use strict'

const React = require('react')

class Content extends React.Component {
  render () {
    let html = this.props.html

    return React.createElement(
      'div',
      { className: 'window__content', dangerouslySetInnerHTML: { __html: html } },
      null
    )
  }
}

Content.propTypes = {
  html: React.PropTypes.string.isRequired
}

module.exports = Content
