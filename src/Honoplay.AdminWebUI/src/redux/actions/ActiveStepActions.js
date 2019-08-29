import {
    INCREMENT,
    DECREMENT,
    RESET
} from '../helpers/ActiveStepHelpers';

export const increment = activeStep => {
    const num = activeStep + 1
    return {
        type: INCREMENT,
        activeStep: num
    }
}

export const decrement = activeStep => {
    const num = activeStep - 1
    return {
        type: DECREMENT,
        activeStep: num
    }
}

export const reset = () => {
    return {
        type: RESET
    }
}