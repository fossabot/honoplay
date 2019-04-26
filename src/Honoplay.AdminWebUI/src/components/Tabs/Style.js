export const Style = (theme) => ({
    root: {
      backgroundColor: theme.palette.background.paper,
      width: '%100',
      flexGrow: 1,
    },
    default_tabStyle: {
        textTransform: 'capitalize',
        borderBottomColor: '#9e9e9e',
        borderBottomStyle: 'solid',
        borderBottomWidth: 1,
    },
    active_tab: {
        borderRadius: 3,
        borderColor: '#9e9e9e',
        borderStyle: 'solid',
        borderBottom: 'none',
        borderWidth: 1,
        textTransform: 'capitalize',
        color: '#673ab7',
        minWidth: 72,
    },
});

import {createMuiTheme} from '@material-ui/core';
export const theme = createMuiTheme({
  palette: {
    secondary: {
      main: '#fafafa'
    }
  },
  typography: {
    useNextVariants: true,
  },
});
