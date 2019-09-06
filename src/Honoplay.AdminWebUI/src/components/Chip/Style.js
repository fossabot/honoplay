const Style = (theme) => ({
    chipRoot: {
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing(0.5),
      height: 100
    },
    chip: {
      margin: theme.spacing(0.5),
    },
    chipTypography: {
      margin: theme.spacing(1),
      color: '#495057',
      fontWeight: 'bold',
    },
  });

  export default Style; 