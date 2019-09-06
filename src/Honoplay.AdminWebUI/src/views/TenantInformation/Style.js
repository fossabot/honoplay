import {green, deepPurple, amber} from '@material-ui/core/colors';
import {createMuiTheme} from '@material-ui/core';
export const Style = (theme) => ({
    root: {
      flexGrow: 1,
      padding: 8 * 3
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
      boxShadow: '0 .5rem 1rem rgba(0,128,0,1)',
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
      margin: theme.spacing(1),
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
    workingStatusDiv: {
      maxHeight: 200,
      overflow: 'auto',
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing(1),
      paddingBottom: 20,
      borderRadius: 3,
      borderColor: '#e0e0e0',
      borderStyle: 'solid',
      borderWidth: 1,
    },
    workingStatusList: {
      paddingLeft: 20,
      paddingTop: 5
    },
    addProgress: {
      color: green[500],
      position: 'absolute',
      marginTop: -30,
      marginLeft: 18
    },
    loadingProgress: {
      color: green[500],
      position: 'absolute',
      marginTop: -20,
      marginLeft: 420
    },
    searchBar: {
      width:'100%'
    }
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


