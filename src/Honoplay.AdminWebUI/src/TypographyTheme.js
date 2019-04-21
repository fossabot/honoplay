import {createMuiTheme} from '@material-ui/core';
import {deepPurple, deepOrange} from '@material-ui/core/colors';
const TypographyTheme = createMuiTheme({
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

export default TypographyTheme;