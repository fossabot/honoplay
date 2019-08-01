import { deepPurple, deepOrange } from '@material-ui/core/colors';
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
        marginRight: theme.spacing.unit,
    },
    instructions: {
        marginTop: theme.spacing.unit,
        marginBottom: theme.spacing.unit,
    },
    nextButton: {
        color: 'white'
    }
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


