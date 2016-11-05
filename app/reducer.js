'use strict'

const actions = require('./actions')

const contentReducer = (state, action) => {
  if (!state) state = { html: '', updatedAt: null }

  switch (action.type) {
    case actions.CONTENT_CHANGED:
      return { html: action.html, updatedAt: Date.now() }
    default:
      return state
  }
}

module.exports = contentReducer
