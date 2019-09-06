import { deepPurple, deepOrange, green } from '@material-ui/core/colors';
import { createMuiTheme } from '@material-ui/core';
export const Style = (theme) => ({
    root: {
        flexGrow: 1,
        width: '90%',
    },
    stepper: {
        backgroundColor: '#fafafa'
    },
    backButton: {
        marginRight: theme.spacing(1),
    },
    instructions: {
        marginTop: theme.spacing(1),
        marginBottom: theme.spacing(1),
    },
    nextButton: {
        color: 'white'
    },
    buttonProgress: {
        color: green[500],
        position: 'absolute',
        marginTop: 6,
        marginLeft: -60
    },
});

export const theme = createMuiTheme({
    overrides: {
        MuiStepIcon: {
            root: {
                '&$completed': {
                    color: '#ffccbc',
                },
                '&$active': {
                    color: '#ff7043',
                },
            },
            active: {},
            completed: {},
        }
    },
    palette: {
        primary: deepPurple,
        secondary: {
            main: deepOrange[300]
        }
    },
});


