'use strict'

const React = require('react')
const ReactDOM = require('react-dom')
const ReactRedux = require('react-redux')
const Redux = require('redux')
const Window = require('../components/window')

const actions = require('../actions')
const createStore = require('../store')

const mapStateToProps = state => {
  return { html: state.html, updatedAt: state.updatedAt }
}

const mapDispatchToProps = dispatch => {
  return { actions: Redux.bindActionCreators(actions, dispatch) }
}

const ConnectedWindow = ReactRedux.connect(
  mapStateToProps,
  mapDispatchToProps
)(Window)

module.exports = {
  render: (file, container) => {
    let store = createStore()
    let element = React.createElement(
      ConnectedWindow,
      { store, file }
    )

    ReactDOM.render(element, container)
  }
}
