import React from 'react'

const Content = (props) => {
  const html = { __html: props.html }
  return <div className='window__content' dangerouslySetInnerHTML={html} />
}

Content.propTypes = {
  html: React.PropTypes.string.isRequired
}

export default Content
