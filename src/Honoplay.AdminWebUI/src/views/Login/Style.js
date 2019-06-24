import {deepPurple, deepOrange} from '@material-ui/core/colors';
export const Style = (theme) => ({
    main: {
      width: 'auto',
      display: 'block', 
      marginLeft: theme.spacing.unit * 3,
      marginRight: theme.spacing.unit * 3,
      [theme.breakpoints.up(400 + theme.spacing.unit * 3 * 2)]: {
        width: 400,
        marginLeft: 'auto',
        marginRight: 'auto',
      },
    },
    paper: {
      marginTop: theme.spacing.unit * 8,
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      padding: `${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px ${theme.spacing.unit * 3}px`,
    },
    avatar: {
      margin: theme.spacing.unit,
      backgroundColor: deepPurple[700]
    },
    form: {
      width: '100%', 
      marginTop: theme.spacing.unit,
    },
    button: {
      marginTop: theme.spacing.unit * 3,
            color: theme.palette.getContrastText(deepPurple[500]),
      backgroundColor: deepPurple[700],
      '&:hover': {
        backgroundColor: deepPurple[700],
      },
      textTransform: 'capitalize',
    },
    marginInput: {
      margin: theme.spacing.unit,
    },
    progress: {
      margin: theme.spacing.unit * 2,
    },    
  });

import {createMuiTheme} from '@material-ui/core';
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
