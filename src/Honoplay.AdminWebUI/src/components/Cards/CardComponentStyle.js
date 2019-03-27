const Style = (theme) => ({
    card: {
      paddingBottom: 37,
      margin: theme.spacing.unit / 2,
    },
    typography: {
      color: '#707070',
      textAlign: 'center',
      fontSize: 15
    },
    typography2: {
      color: '#8d8d8d',
      textAlign: 'center',
      fontSize:12
    },
    center: {
      display: 'flex',
      justifyContent: 'center'
    },
    icon: {
      display: 'flex',
      justifyContent: 'flex-end'
    },
    chip: {
      margin: theme.spacing.unit / 2,
    },
    root: {
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing.unit / 2,
    },
  });

  export default Style;