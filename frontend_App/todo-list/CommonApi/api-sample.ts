import axios from 'axios'

function getSample () {
  const path = '/sample.json'
  return axios.get(path)
}

export default {
  getSample
}
