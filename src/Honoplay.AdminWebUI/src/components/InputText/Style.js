const Style = (theme) => ({
    root: {
      flexGrow: 1,
    },
    bootstrapRoot: {
      'label + &': {
      marginTop: theme.spacing.unit * 20,
      },
    },
    bootstrapInput: {
      borderRadius: 4,
      position: 'relative',
      backgroundColor: theme.palette.common.white,
      border: '1px solid #ced4da',
      fontSize: 16,
      width: '90%',
      padding: '13px 12px',
      transition: theme.transitions.create(['border-color', 'box-shadow']),
      // Use the system font instead of the default Roboto font.
      fontFamily: [
        '-apple-system',
        'BlinkMacSystemFont',
        '"Segoe UI"',
        'Roboto',
        '"Helvetica Neue"',
        'Arial',
        'sans-serif',
        '"Apple Color Emoji"',
        '"Segoe UI Emoji"',
        '"Segoe UI Symbol"',
      ].join(','),
      '&:focus': {
        borderRadius: 4,
        borderColor: '#80bdff',
        boxShadow: '0 0 0 0.2rem rgba(0,123,255,.25)'
      },
    },
    bootstrapFormLabel: {
      fontSize: 17,
      color: '#495057'
    },
    center: {
      marginTop: 10
    }
});

export default Style;