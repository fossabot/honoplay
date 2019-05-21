import axios from 'axios';

export const FETCH_SORULAR = "FETCH_SORULAR";

export function fetchSorular() {
    return dispatch => {
        dispatch ({
            type: "FETCH_SORULAR",
            payload: axios.get(`https://jsonplaceholder.typicode.com/users`)
            .then(result => console.log(result))
        })
    }
}