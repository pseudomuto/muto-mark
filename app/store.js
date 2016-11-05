'use strict'

const redux = require('redux')
const thunk = require('redux-thunk')
const reducer = require('./reducer')

module.exports = (initialState) => {
  return redux.createStore(
    reducer,
    initialState,
    redux.applyMiddleware(thunk.default)
  )
}
