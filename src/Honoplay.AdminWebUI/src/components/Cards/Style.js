const Style = (theme) => ({
    chipRoot: {
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing.unit / 20,
      paddingBottom: 20
    },
    chip: {
      margin: theme.spacing.unit / 1,
    },
    chipTypography: {
      margin: theme.spacing.unit,
      color: '#495057',
      paddingBottom: 20
    },
  });

  export default Style;