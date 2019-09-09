import {deepPurple, deepOrange} from '@material-ui/core/colors';
import {createMuiTheme} from '@material-ui/core';

export const Style = (theme) => ({
    main: {
      width: 'auto',
      display: 'block', 
      marginLeft: theme.spacing(1) * 3,
      marginRight: theme.spacing(1) * 3,
      [theme.breakpoints.up(400 + theme.spacing(1) *6)]: {
        width: 400,
        marginLeft: 'auto',
        marginRight: 'auto',
      },
    },
    paper: {
      marginTop: theme.spacing(1) * 8,
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      padding: `${theme.spacing(1) * 2}px ${theme.spacing(1) * 3}px ${theme.spacing(1) * 3}px`,
    },
    avatar: {
      margin: theme.spacing(1),
      backgroundColor: deepPurple[700]
    },
    form: {
      width: '100%', 
      marginTop: theme.spacing(1),
    },
    button: {
      marginTop: theme.spacing(1) * 3,
            color: theme.palette.getContrastText(deepPurple[500]),
      backgroundColor: deepPurple[700],
      '&:hover': {
        backgroundColor: deepPurple[700],
      },
      textTransform: 'capitalize',
    },
    marginInput: {
      margin: theme.spacing(1),
    },
    progress: {
      margin: theme.spacing(1) * 2,
    },    
  });

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
