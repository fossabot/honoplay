import { green, amber } from '@material-ui/core/colors';

export const Style = (theme) => ({
    success: {
        backgroundColor: green[600],
    },
    error: {
        backgroundColor: theme.palette.error.dark,
    },
    info: {
        backgroundColor: theme.palette.primary.dark,
    },
    warning: {
        backgroundColor: amber[700],
    },
    snackbarIcon: {
        fontSize: 20,
    },
    snackbarIconVariant: {
        opacity: 0.9,
        marginRight: theme.spacing.unit,
    },
    snackbarMessage: {
        display: 'flex',
        alignItems: 'center',
    },
    tabContainer: {
        padding: 8 * 3
    }
});
