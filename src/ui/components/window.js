import React, { Component } from 'react'
import Content from './content'

class Window extends Component {
  componentDidMount () {
    this.props.actions.convertFileToHTML(this.props.file)
  }

  render () {
    const { html } = this.props

    return (
      <div className='window'>
        <Content html={html} />
      </div>
    )
  }
}

Window.propTypes = {
  file: React.PropTypes.string.isRequired
}

export default Window
