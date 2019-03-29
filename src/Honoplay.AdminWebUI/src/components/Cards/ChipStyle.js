const Style = (theme) => ({
    root: {
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing.unit / 2,
      backgroundColor: '#eaeaea',
    },
    chip: {
      margin: theme.spacing.unit / 2,
    },
    typography: {
      margin: theme.spacing.unit,
      color: '#495057',
      fontWeight: 'bold',
    },
  });

  export default Style;