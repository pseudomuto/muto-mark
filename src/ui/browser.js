'use strict'

import { createElement } from 'react'
import { render } from 'react-dom'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import Window from './components/window'
import { convertFileToHTML } from '../actions'
import createStore from '../store'

const mapStateToProps = state => {
  const { html, updatedAt } = state
  return { html, updatedAt }
}

const mapDispatchToProps = dispatch => {
  return { actions: bindActionCreators({ convertFileToHTML }, dispatch) }
}

const ConnectedWindow = connect(mapStateToProps, mapDispatchToProps)(Window)

export default (file, container) => {
  const store = createStore()
  const element = createElement(ConnectedWindow, { store, file })

  render(element, container)
}
