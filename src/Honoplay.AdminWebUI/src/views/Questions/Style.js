import { green } from '@material-ui/core/colors';
export const Style = theme => ({
    buttonProgress: {
        color: green[500],
        position: 'absolute',
        marginTop: -30,
        marginLeft: 25
    },
    checkbox: {
        height: 30,
        width: 30
    },
    root: {
        flexGrow: 1,
        padding: 8 * 3
    },
});


import { createMuiTheme } from '@material-ui/core';
import { deepPurple, deepOrange } from '@material-ui/core/colors';
export const theme = createMuiTheme({
    palette: {
        primary: deepPurple,
        secondary: {
            main: deepOrange[300]
        }
    },
    typography: {
        useNextVariants: true,
    },
});