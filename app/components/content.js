'use strict'

const React = require('react')

const Content = (props) => {
  let html = props.html

  return React.createElement(
    'div',
    { className: 'window__content', dangerouslySetInnerHTML: { __html: html } },
    null
  )
}

Content.propTypes = {
  html: React.PropTypes.string.isRequired
}

module.exports = Content
