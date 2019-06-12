import {green, deepPurple} from '@material-ui/core/colors';
import {createMuiTheme} from '@material-ui/core';
export const Style = (theme) => ({
    root: {
      flexGrow: 1,
    },
    default_tabStyle: {
        textTransform: 'capitalize',
        borderBottomColor: '#9e9e9e',
        borderBottomStyle: 'solid',
        borderBottomWidth: 1,
        color: '#673ab7',
        fontSize: 14
    },
    active_tab: {
        borderRadius: 3,
        borderColor: '#9e9e9e',
        borderStyle: 'solid',
        borderBottom: 'none',
        borderWidth: 1,
        textTransform: 'capitalize',
        color: '#673ab7',
        fontSize: 14
    },
    buttonSuccess: {
      backgroundColor: green[500],
      '&:hover': {
        backgroundColor: green[700],
      },
    },
    buttonSave: {
      backgroundColor: deepPurple,
      '&:hover': {
        backgroundColor: deepPurple
      },
    },
    buttonProgress: {
      color: green[500],
      position: 'absolute',
      top: '50%',
      left: '50%',
      marginTop: -12,
      marginLeft: -12,
    },
    buttonWrapper: {
      margin: theme.spacing.unit,
      position: 'relative',
    },
    buttonRoot: {
      display: 'flex',
      alignItems: 'center',
    },
    fabProgress: {
      color: green[500],
      position: 'absolute',
      top: -3,
      left: -4,
      zIndex: 1,
    },
});

export const theme = createMuiTheme({
  palette: {
    primary: deepPurple,
    secondary: {
      main: '#fafafa'
    }
  },
  typography: {
    useNextVariants: true,
  },
});
